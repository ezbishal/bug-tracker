import React, { FC, useState } from 'react';
import { BrowserRouter, Route, Routes, Navigate} from 'react-router-dom';
import './App.css';
import Dashboard from './Components/Dashboard';
import Login from './Components/Login';

const App: FC = () => {

  const [token, setToken] = useState<string>("");
  return (
      <BrowserRouter>
        <Routes>
          <Route path='login' element={ token !== "" ? <Navigate replace to={'/'} /> : <Login setToken={setToken}/> } />
          <Route path='/' element={ token !== "" ? <Dashboard /> : <Navigate replace to={'/login'} /> } />
        </Routes>
      </BrowserRouter>    
    );
}

export default App;