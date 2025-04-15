package com.example.tecbank;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;
import java.util.List;

public class MovimientoAdapter extends RecyclerView.Adapter<MovimientoAdapter.ViewHolder> {

    private List<Movimiento> movimientos;

    public MovimientoAdapter(List<Movimiento> movimientos) {
        this.movimientos = movimientos;
    }

    public static class ViewHolder extends RecyclerView.ViewHolder {
        TextView tvNumero, tvDescripcion, tvTipo, tvFecha, tvTotal;

        public ViewHolder(View itemView) {
            super(itemView);
            tvNumero = itemView.findViewById(R.id.tvNumeroTarjeta);
            tvDescripcion = itemView.findViewById(R.id.tvDescripcion);
            tvTipo = itemView.findViewById(R.id.tvTipo);
            tvFecha = itemView.findViewById(R.id.tvFecha);
            tvTotal = itemView.findViewById(R.id.tvTotal);
        }
    }

    @NonNull
    @Override
    public MovimientoAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_movimiento, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        Movimiento mov = movimientos.get(position);
        holder.tvNumero.setText(mov.getNumeroTarjeta());
        holder.tvDescripcion.setText(mov.getDescripcion());
        holder.tvTipo.setText(mov.getTipoTransferencia());
        holder.tvFecha.setText(mov.getFecha());
        holder.tvTotal.setText(mov.getTotalTransferido());
    }

    @Override
    public int getItemCount() {
        return movimientos.size();
    }
}