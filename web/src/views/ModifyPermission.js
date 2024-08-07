import React, { useState } from 'react';
import { modifyPermission } from './../services/permiso.service';

const ModifyPermission = () => {
  const [form, setForm] = useState({
    id: '',
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
    modifyPermission(form.id, form).then(() => {
      alert('Permission modified successfully');
    }).catch((error) => {
      alert('Error modifying permission: ' + error.message);
    });
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Modify Permission</h2>
      <input type="number" name="id" placeholder="ID" value={form.id} onChange={handleChange} required />
      <input type="text" name="nombreEmpleado" placeholder="Nombre" value={form.nombreEmpleado} onChange={handleChange} required />
      <input type="text" name="apellidoEmpleado" placeholder="Apellido" value={form.apellidoEmpleado} onChange={handleChange} required />
      <input type="number" name="tipoPermiso" placeholder="Tipo Permiso" value={form.tipoPermiso} onChange={handleChange} required />
      <input type="date" name="fechaPermiso" value={form.fechaPermiso} onChange={handleChange} required />
      <button type="submit">Modify Permission</button>
    </form>
  );
};

export default ModifyPermission;
