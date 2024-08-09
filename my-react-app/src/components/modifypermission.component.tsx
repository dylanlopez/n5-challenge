import React, { useState, useEffect } from 'react';
import { modifyPermission } from './../services/permiso.service';
import { Permiso } from '../dtos/permiso';

interface ModifyPermissionProps {
  permiso: Permiso;
  onModifyCompleted: () => void;
}

const ModifyPermission: React.FC<ModifyPermissionProps> =  ({ permiso, onModifyCompleted }) => {
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
        alert('Modificado correctamente');
        onModifyCompleted(); // Ocultar el formulario
      }
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input type="text" name="empleadoNombre" value={permisoModificado.empleadoNombre} onChange={handleChange} placeholder="NombreEmpleado" />
      <input type="text" name="empleadoApellido" value={permisoModificado.empleadoApellido} onChange={handleChange} placeholder="ApellidoEmpleado" />
      <input type="date" name="fechaPermiso" value={permisoModificado.fechaPermiso} onChange={handleChange} />
      <input type="text" name="tipoPermisoId" value={permisoModificado.tipoPermisoId} onChange={handleChange} placeholder="TipoPermiso" />
      <button type="submit">Modify Permission</button>
    </form>
  );
};

export default ModifyPermission;
