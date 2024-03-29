import React, { FC } from 'react';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import './App.css';
import Dashboard from './Components/Dashboard';
import Login from './Components/Login';
import { AuthProvider } from './Services/AuthProvider';
import ProjectDetails from './Components/ProjectDetails';
import Register from './Components/Register';
import axios from 'axios';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

const setSession = (userToken: string): void => {
  sessionStorage.setItem('token', userToken);
}

const getSession = (): string => {
  const token: string | null = sessionStorage.getItem('token');
  return token ?? '';
}

const token = sessionStorage.getItem("token") ?? '';
axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;

const App: FC = () => {

  return (
    <AuthProvider>
     <BrowserRouter>
      <Routes>
        <Route path='login' element={ getSession() !== '' ? <Navigate replace to={'/'} /> : <Login setToken={setSession} />} />
        <Route path='/' element={ getSession() !== '' ? <Dashboard /> : <Navigate replace to={'/login'} />} />
        <Route path='/projects/:projectId' element={<ProjectDetails />} />
        <Route path='/register' element={<Register />} />
      </Routes>
     </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
