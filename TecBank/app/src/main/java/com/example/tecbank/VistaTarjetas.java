package com.example.tecbank;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

public class VistaTarjetas extends AppCompatActivity {

    private RecyclerView recyclerView;
    private TarjetaAdapter adapter;
    private List<Tarjeta> tarjetaList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.vista_tarjetas); // Lo creamos en el paso 3

        recyclerView = findViewById(R.id.recyclerTarjetas);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        tarjetaList = new ArrayList<>();
        tarjetaList.add(new Tarjeta("1234-5678-9012-3456", "Pago Recibos", "15/03/2025", "-€50.000"));
        tarjetaList.add(new Tarjeta("0000-0000-0000-0000", "Intereses Ganados", "15/05/2025", "€85,67"));
        tarjetaList.add(new Tarjeta("1234-5678-9012-3456", "Transferencia", "15/03/1945", "€8.500"));
        tarjetaList.add(new Tarjeta("0000-0000-0000-0000", "Pago Trabajos", "29/02/2025", "-€6.000"));

        adapter = new TarjetaAdapter(tarjetaList);
        recyclerView.setAdapter(adapter);
    }
}
