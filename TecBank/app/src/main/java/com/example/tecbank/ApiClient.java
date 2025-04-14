package com.example.tecbank;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Handler;
import android.os.Looper;
import okhttp3.*;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import okhttp3.MediaType;


import java.io.IOException;

public class ApiClient extends AppCompatActivity {
    private static final String BASE_URL = "http://45.63.12.34:5000/v1"; // Usa tu IP o dominio real
    private static final OkHttpClient client = new OkHttpClient();

    public interface Callback {
        void onSuccess(String response);
        void onError(String error);
    }

    public static void login(String usuario, String password, Callback callback) {
        String url = BASE_URL + "/auth/login";

        MediaType JSON = MediaType.parse("application/json; charset=utf-8");
        String jsonBody = "{\"usuario\":\"" + usuario + "\",\"password\":\"" + password + "\"}";

        RequestBody body = RequestBody.create(jsonBody, JSON);
        Request request = new Request.Builder()
                .url(url)
                .post(body)
                .build();

        client.newCall(request).enqueue(new okhttp3.Callback() {
            @Override
            public void onFailure(Call call, IOException e) {
                new Handler(Looper.getMainLooper()).post(() -> callback.onError("Error de conexión: " + e.getMessage()));
            }

            @Override
            public void onResponse(Call call, Response response) throws IOException {
                if (response.isSuccessful()) {
                    final String responseData = response.body().string();
                    new Handler(Looper.getMainLooper()).post(() -> callback.onSuccess(responseData));
                } else {
                    new Handler(Looper.getMainLooper()).post(() -> callback.onError("Error: " + response.code()));
                }
            }
        });
    }
}


