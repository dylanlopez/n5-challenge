import React, { useEffect, useState } from 'react';
import { getPermissions } from './../services/permiso.service';
import { Permiso } from '../dtos/permiso';
import RequestPermission from './requestpermission.component';
import ModifyPermission from './modifypermission.component';

const GetPermissions: React.FC = () => {
  const [permissions, setPermissions] = useState<Permiso[]>([]);
  const [showRequestForm, setShowRequestForm] = useState<boolean>(false);
  const [showModifyForm, setShowModifyForm] = useState<boolean>(false);
  const [selectedPermiso, setSelectedPermission] = useState<Permiso | null>(null);

  useEffect(() => {
    const fetchPermissions = async () => {
      try {
        const response = await getPermissions();
        setPermissions(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchPermissions();
  }, []);

  const fetchPermissions = async () => {
    try {
      const response = await getPermissions();
      setPermissions(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleRequestClick = () => {
    setShowRequestForm(true);
    setShowModifyForm(false);
    setSelectedPermission(null);
  };

  const handleModifyClick = (permission: Permiso) => {
    setSelectedPermission(permission);
    setShowRequestForm(false);
    setShowModifyForm(true);
  };

  const handleRequestCompleted = () => {
    setShowRequestForm(false);
    fetchPermissions();
  };

  return (
    <div>
    <h1>Permissions</h1>
    <button onClick={handleRequestClick}>Add New Permission</button>
    {showRequestForm && <RequestPermission onRequestCompleted={handleRequestCompleted} />}
    {showModifyForm && selectedPermiso && <ModifyPermission permiso={selectedPermiso} />}
    <ul>
      {permissions.map(permiso => (
        <li key={permiso.id}>
          {`${permiso.empleadoNombre} ${permiso.empleadoApellido} - ${permiso.fechaPermiso} - ${permiso.tipoPermisoId}`}
          <button onClick={() => handleModifyClick(permiso)}>Modify</button>
        </li>
      ))}
    </ul>
  </div>
  );
};

export default GetPermissions;
