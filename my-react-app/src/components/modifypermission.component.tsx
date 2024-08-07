import React, { useState, useEffect } from 'react';
import { modifyPermission } from './../services/permiso.service';
import { Permiso } from '../dtos/permiso';

interface ModifyPermissionProps {
  permiso: Permiso;
}

const ModifyPermission: React.FC<ModifyPermissionProps> =  ({ permiso }) => {
  const [permisoModificado, setModifiedPermission] = useState<Permiso>(permiso);

  useEffect(() => {
    setModifiedPermission(permiso);
  }, [permiso]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setModifiedPermission({ ...permisoModificado, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (permisoModificado.id) {
        const response = await modifyPermission(permisoModificado.id, permisoModificado);
        console.log(response.data);
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input type="text" name="NombreEmpleado" value={permisoModificado.empleadoNombre} onChange={handleChange} placeholder="NombreEmpleado" />
      <input type="text" name="ApellidoEmpleado" value={permisoModificado.empleadoApellido} onChange={handleChange} placeholder="ApellidoEmpleado" />
      <input type="date" name="FechaPermiso" value={permisoModificado.fechaPermiso} onChange={handleChange} />
      <input type="text" name="TipoPermiso" value={permisoModificado.tipoPermisoId} onChange={handleChange} placeholder="TipoPermiso" />
      <button type="submit">Modify Permission</button>
    </form>
  );
};

export default ModifyPermission;
