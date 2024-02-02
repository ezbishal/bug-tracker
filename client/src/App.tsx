import React, { FC, useEffect, useState } from 'react';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import './App.css';
import Dashboard from './Components/Dashboard';
import Login from './Components/Login';

const setToken = (userToken: string): void => {
  sessionStorage.setItem('token', userToken);
}

const App: FC = () => {

  const[token, setToken] = useState<string | null>(null)

  useEffect(()=>{
    const getToken = async (): Promise<void> => {
      const fetchedToken: string | null = await sessionStorage.getItem('token');
      setToken(fetchedToken || null);
    }

    getToken()
  }, [])

  return (
    <BrowserRouter>
      <Routes>
        <Route path='/login' element={ token !== null ? <Navigate replace to={'/'} /> : <Login setToken={setToken} />} />
        <Route path='/' element={ token !== null ? <Dashboard /> : <Navigate replace to={'/login'} />} />
      </Routes>
    </BrowserRouter>
  );
}


export default App;
