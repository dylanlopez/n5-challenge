import React, { useEffect, useState } from 'react';
import { getPermissions } from './../services/permiso.service';

const ListPermissions = () => {
  const [permissions, setPermissions] = useState([]);

  useEffect(() => {
    getPermissions().then((response) => {
      setPermissions(response.data);
    });
  }, []);

  return (
    <div>
      <h2>Permissions</h2>
      <ul>
        {permissions.map((permission) => (
          <li key={permission.id}>
            {permission.nombreEmpleado} {permission.apellidoEmpleado} - {permission.tipoPermiso} - {new Date(permission.fechaPermiso).toLocaleDateString()}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ListPermissions;
