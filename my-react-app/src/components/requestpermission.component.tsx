import React, { useState } from 'react';
import { requestPermission } from './../services/permiso.service';
import { Permiso } from '../dtos/permiso';

interface RequestPermissionProps {
  onRequestCompleted: () => void;
}

const RequestPermission: React.FC<RequestPermissionProps> = ({ onRequestCompleted }) => {
  const [permiso, setPermission] = useState<Permiso>({ empleadoNombre: '', empleadoApellido: '', fechaPermiso: '', tipoPermisoId: '' });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setPermission({ ...permiso, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    console.log('handleSubmit');
    e.preventDefault();
    try {
      console.log(' permiso: ', permiso);
      const response = await requestPermission(permiso);
      console.log(response.data);
      alert('Creado correctamente');
      onRequestCompleted(); // Ocultar el formulario
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input type="text" name="empleadoNombre" value={permiso.empleadoNombre} onChange={handleChange} placeholder="NombreEmpleado" />
      <input type="text" name="empleadoApellido" value={permiso.empleadoApellido} onChange={handleChange} placeholder="ApellidoEmpleado" />
      <input type="date" name="fechaPermiso" value={permiso.fechaPermiso} onChange={handleChange} />
      <input type="text" name="tipoPermisoId" value={permiso.tipoPermisoId} onChange={handleChange} placeholder="TipoPermiso" />
      <button type="submit">Request Permission</button>
    </form>
  );
};

export default RequestPermission;
