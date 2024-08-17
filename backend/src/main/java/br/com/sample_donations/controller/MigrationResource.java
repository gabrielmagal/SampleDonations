package br.com.sample_donations.controller;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.transaction.Transactional;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.QueryParam;
import org.flywaydb.core.Flyway;

import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;

@Path("/migrate")
@ApplicationScoped
public class MigrationResource {
    @Inject
    DataSource dataSource;

    @POST
    public String migrate(@QueryParam("schema") String schema) {
        if (schema == null || schema.isEmpty()) {
            return "É necessário informar um schema para migration.";
        }

        try (Connection connection = dataSource.getConnection()) {
            if (!schemaExists(connection, schema)) {
                createSchema(connection, schema);
            }
        } catch (Exception e) {
            e.printStackTrace();
            return "Migration falhou para o schema '" + schema + "' - Error: " + e.getMessage();
        }

        Flyway.configure()
                .dataSource(dataSource)
                .schemas(schema)
                .load()
                .migrate();

        return "Migration aplicada com suceso no schema " + schema;
    }

    public boolean schemaExists(Connection connection, String schema) throws Exception {
        String checkSchemaQuery = "SELECT schema_name FROM information_schema.schemata WHERE schema_name = ?";
        try (var pstmt = connection.prepareStatement(checkSchemaQuery)) {
            pstmt.setString(1, schema);
            try (ResultSet rs = pstmt.executeQuery()) {
                return rs.next();
            }
        }
    }

    @Transactional(Transactional.TxType.NOT_SUPPORTED)
    public void createSchema(Connection connection, String schema) throws Exception {
        String createSchemaQuery = "CREATE SCHEMA IF NOT EXISTS " + schema;
        try (Statement stmt = connection.createStatement()) {
            stmt.executeUpdate(createSchemaQuery);
        }
    }
}