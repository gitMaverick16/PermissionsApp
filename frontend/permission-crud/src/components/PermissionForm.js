import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createPermission, updatePermission, getPermissionById } from '../api';
import './PermissionForm.css';

const PermissionForm = () => {
  const [permission, setPermission] = useState({
    employeeName: '',
    employeeLastName: '',
    permissionDate: '',
    permissionTypeId: 1,
  });

  const today = new Date().toISOString().split("T")[0];
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');
  const [messageType, setMessageType] = useState(''); // 'success' o 'error'
  
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      setLoading(true);
      getPermissionById(id)
        .then((response) => setPermission(response.data))
        .catch(() => showMessage('Error fetching permission data', 'error'))
        .finally(() => setLoading(false));
    }
  }, [id]);

  const handleSubmit = (e) => {
    e.preventDefault();
    setLoading(true);

    const apiCall = id ? updatePermission(id, permission) : createPermission(permission);

    apiCall
      .then(() => {
        showMessage(`Permission ${id ? 'updated' : 'created'} successfully`, 'success');
        setTimeout(() => navigate('/'), 2000); // Redirigir después de 2s
      })
      .catch(() => showMessage(`Error ${id ? 'updating' : 'creating'} permission`, 'error'))
      .finally(() => setLoading(false));
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPermission({ ...permission, [name]: value });
  };

  const showMessage = (text, type) => {
    setMessage(text);
    setMessageType(type);
    setTimeout(() => setMessage(''), 3000); // Ocultar el mensaje después de 3s
  };

  return (
    <div className="permission-form">
      <h2>{id ? 'Edit Permission' : 'Add Permission'}</h2>

      {loading && (
        <div className="spinner">
          <div className="lds-dual-ring"></div>
        </div>
      )}

      {message && (
        <div className={`alert ${messageType}`}>
          {message}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="employeeName"
          value={permission.employeeName}
          onChange={handleChange}
          placeholder="Employee Name"
          required
        />
        <input
          type="text"
          name="employeeLastName"
          value={permission.employeeLastName}
          onChange={handleChange}
          placeholder="Employee Last Name"
          required
        />
        <input
          type="date"
          min={today}
          name="permissionDate"
          value={permission.permissionDate}
          onChange={handleChange}
          required
        />
        <select
          name="permissionTypeId"
          value={permission.permissionTypeId}
          onChange={handleChange}
          required
        >
          <option value="1">Sick Leave</option>
          <option value="2">Vacation</option>
          <option value="3">Maternity Leave</option>
          <option value="4">Paternity Leave</option>
          <option value="5">Personal Leave</option>
        </select>
        <button type="submit">{id ? 'Update Permission' : 'Add Permission'}</button>
      </form>
    </div>
  );
};

export default PermissionForm;
