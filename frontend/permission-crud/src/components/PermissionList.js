import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getPermissions, deletePermission } from '../api';
import './PermissionList.css'; 

const PermissionList = () => {
  const [permissions, setPermissions] = useState([]);

  useEffect(() => {
    getPermissions()
      .then((response) => setPermissions(response.data))
      .catch((error) => console.error('Error fetching permissions:', error));
  }, []); 

  const refreshPermissions = () => {
    getPermissions()
      .then((response) => setPermissions(response.data))
      .catch((error) => console.error('Error fetching permissions:', error));
  };

  const handleDelete = (id) => {
    deletePermission(id)
      .then(() => {
        setPermissions(permissions.filter((perm) => perm.id !== id));
      })
      .catch((error) => console.error('Error deleting permission:', error));
  };

  return (
    <div className="permission-list">
      <h2>Permissions</h2>
      <Link to="/add">Add New Permission</Link>
      <ul>
        {permissions.map((permission) => (
          <li key={permission.id}>
            <div>
              <p>{permission.employeeName} {permission.employeeLastName}</p>
              <p>{permission.permissionDate}</p>
              <p>{permission.permissionTypeDescription}</p>
            </div>
            <div className="actions">
              <Link to={`/edit/${permission.id}`} onClick={refreshPermissions}>Edit</Link>
              <button onClick={() => handleDelete(permission.id)}>Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PermissionList;
