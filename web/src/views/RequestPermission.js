import React, { useState } from 'react';
import { requestPermission } from './../services/permiso.service';

const RequestPermission = () => {
  const [form, setForm] = useState({
    nombreEmpleado: '',
    apellidoEmpleado: '',
    tipoPermiso: 0,
    fechaPermiso: ''
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm({ ...form, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    requestPermission(form).then(() => {
      alert('Permission requested successfully');
    }).catch((error) => {
      alert('Error requesting permission: ' + error.message);
    });
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Request Permission</h2>
      <input type="text" name="nombreEmpleado" placeholder="Nombre" value={form.nombreEmpleado} onChange={handleChange} required />
      <input type="text" name="apellidoEmpleado" placeholder="Apellido" value={form.apellidoEmpleado} onChange={handleChange} required />
      <input type="number" name="tipoPermiso" placeholder="Tipo Permiso" value={form.tipoPermiso} onChange={handleChange} required />
      <input type="date" name="fechaPermiso" value={form.fechaPermiso} onChange={handleChange} required />
      <button type="submit">Request Permission</button>
    </form>
  );
};

export default RequestPermission;
