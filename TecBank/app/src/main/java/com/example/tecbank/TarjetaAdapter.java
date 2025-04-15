package com.example.tecbank;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

public class TarjetaAdapter extends RecyclerView.Adapter<TarjetaAdapter.TarjetaViewHolder> {

    private List<Tarjeta> tarjetaList;

    public TarjetaAdapter(List<Tarjeta> tarjetaList) {
        this.tarjetaList = tarjetaList;
    }

    @NonNull
    @Override
    public TarjetaViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_tarjeta, parent, false);
        return new TarjetaViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull TarjetaViewHolder holder, int position) {
        Tarjeta tarjeta = tarjetaList.get(position);
        holder.numero.setText(tarjeta.getNumero());
        holder.descripcion.setText(tarjeta.getDescripcion());
        holder.fecha.setText(tarjeta.getFecha());
        holder.total.setText(tarjeta.getTotal());


    }

    @Override
    public int getItemCount() {
        return tarjetaList.size();
    }

    public static class TarjetaViewHolder extends RecyclerView.ViewHolder {
        TextView numero, descripcion, fecha, total;

        public TarjetaViewHolder(@NonNull View itemView) {
            super(itemView);
            numero = itemView.findViewById(R.id.textNumeroTarjeta);
            descripcion = itemView.findViewById(R.id.textDescripcion);
            fecha = itemView.findViewById(R.id.textFecha);
            total = itemView.findViewById(R.id.textTotal);
        }
    }
}
