import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import PermissionList from './components/PermissionList';
import PermissionForm from './components/PermissionForm';

const App = () => {
  return (
    <Router>
      <div className="app">
        <Routes>
          <Route path="/" element={<PermissionList />} />
          <Route path="/add" element={<PermissionForm />} />
          <Route path="/edit/:id" element={<PermissionForm />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;