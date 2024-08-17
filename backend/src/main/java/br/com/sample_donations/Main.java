package br.com.sample_donations;

import br.com.sample_donations.controller.dto.ClientSecretDto;
import io.quarkus.runtime.Quarkus;
import io.quarkus.runtime.QuarkusApplication;
import io.quarkus.runtime.annotations.QuarkusMain;

import java.util.HashMap;

@QuarkusMain
public class Main {
    public static HashMap<String, ClientSecretDto> hashClientSecret = new HashMap<>();

    public static void main(String... args) {
        Quarkus.run(MyApp.class, args);
    }

    public static class MyApp implements QuarkusApplication {

        @Override
        public int run(String... args) {
            hashClientSecret.put("doacoesalimenticias", new ClientSecretDto("doacoesalimenticias-backend", "hXyq24462kDN7SIzRiTfgYyXzb4HamPL"));
            hashClientSecret.put("scdoacoes", new ClientSecretDto("scdoacoes-backend", "vyA6fPKioy1BKlFh15IsBE8Fkv3ZbxqF"));
            hashClientSecret.put("petrodoacoes", new ClientSecretDto("petrodoacoes-backend", "zEbrX0EgGngvYynVAI3RTFSwoA62k1f6"));
            System.out.println("Do startup logic here");
            Quarkus.waitForExit();
            return 0;
        }
    }
}