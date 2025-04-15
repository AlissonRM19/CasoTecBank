package com.example.tecbank;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.List;

public class PrestamoAdapter extends RecyclerView.Adapter<PrestamoAdapter.PrestamoViewHolder> {

    private List<Prestamo> prestamoList;

    public PrestamoAdapter(List<Prestamo> prestamoList) {
        this.prestamoList = prestamoList;
    }

    @NonNull
    @Override
    public PrestamoViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_prestamo, parent, false);
        return new PrestamoViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull PrestamoViewHolder holder, int position) {
        Prestamo p = prestamoList.get(position);
        holder.nombre.setText(p.getNombre());
        holder.descripcion.setText(p.getDescripcion());
        holder.cantidad.setText(p.getCantidad());
        holder.fecha.setText(p.getFecha());
        holder.intereses.setText(p.getIntereses());
        holder.total.setText(p.getTotal());
    }

    @Override
    public int getItemCount() {
        return prestamoList.size();
    }

    public static class PrestamoViewHolder extends RecyclerView.ViewHolder {
        TextView nombre, descripcion, cantidad, fecha, intereses, total;

        public PrestamoViewHolder(@NonNull View itemView) {
            super(itemView);
            nombre = itemView.findViewById(R.id.textNombre);
            descripcion = itemView.findViewById(R.id.textDescripcion);
            cantidad = itemView.findViewById(R.id.textCantidad);
            fecha = itemView.findViewById(R.id.textFecha);
            intereses = itemView.findViewById(R.id.textIntereses);
            total = itemView.findViewById(R.id.textTotal);
        }
    }
}
