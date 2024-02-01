import React, { FC, useState } from 'react';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import './App.css';
import Dashboard from './Components/Dashboard';
import Login from './Components/Login';

const setToken = (userToken: string): void => {
  sessionStorage.setItem('token', userToken);
}

const getToken = (): string => {
  const token: string | null = sessionStorage.getItem('token');
  return token ?? '';
}

const App: FC = () => {

  return (
    <BrowserRouter>
      <Routes>
        <Route path='login' element={ getToken() !== '' ? <Navigate replace to={'/'} /> : <Login setToken={setToken} />} />
        <Route path='/' element={ getToken() !== '' ? <Dashboard /> : <Navigate replace to={'/login'} />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
