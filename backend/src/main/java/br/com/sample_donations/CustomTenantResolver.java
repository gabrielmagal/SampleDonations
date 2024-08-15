package br.com.sample_donations;

import io.quarkus.hibernate.orm.PersistenceUnitExtension;
import io.quarkus.hibernate.orm.runtime.tenant.TenantResolver;
import io.vertx.ext.web.RoutingContext;
import jakarta.enterprise.context.RequestScoped;
import jakarta.inject.Inject;

@PersistenceUnitExtension
@RequestScoped
public class CustomTenantResolver implements TenantResolver {
    @Inject
    RoutingContext context;

    @Override
    public String getDefaultTenantId() {
        return "";
    }

    @Override
    public String resolveTenantId() {
        return context.request().getHeader("X-Tenant");
    }

    @Override
    public boolean isRoot(String tenantId) {
        return TenantResolver.super.isRoot(tenantId);
    }
}