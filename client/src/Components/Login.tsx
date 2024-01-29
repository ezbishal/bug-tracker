import React, { FC, FormEvent, useState } from 'react';
import axios, { AxiosResponse } from 'axios';
import { useNavigate } from 'react-router-dom'

interface LoginProps {
  setToken: (value: string) => void;
}

const Login: FC<LoginProps> = ({ setToken }) => {

  const [username, setUsername] = useState<string>();
  const [password, setPassword] = useState<string>();

  const baseUrl: string = "https://localhost:7109/api";

  const GetToken = async (credentials: object): Promise<string> => {
    try {
      return await axios
        .post<string>(`${baseUrl}/user/token`, credentials)
        .then((response: AxiosResponse<string>) => {
          return response.data;
        });
    } catch (error) {
      console.error("Failed to fetch token:", error);
      return "";
    }
  };
  
  const navigate = useNavigate();

  const handleSubmit = async (
    event: FormEvent<HTMLFormElement>
  ): Promise<void> => {
    event.preventDefault();
    const token = await GetToken({ username, password });
    if(token) {
        setToken(token);
        navigate("/");
    }
  };

  return (
    <>
      <div className="bg-black/50 fixed top-0 left-0 w-full h-screen"></div>
      <div className="fixed w-full px-4 py-24 z-50">
        <div className="max-w-[450px] h-[600px] mx-auto bg-black/80 text-white">
          <div className="max-w-[320px] mx-auto py-16">
            <h1>Login</h1>
            <form onSubmit={handleSubmit} className="w-full flex flex-col py-4">
              <label htmlFor="username" className="text-white font-bold">
                Username
              </label>
              <input
                onChange={(e) => setUsername(e.target.value)}
                id="username"
                type="text"
                required
                className="p-3 my-2 rounded text-black"
                placeholder="Your username"
              />
              <label htmlFor="password" className="text-white font-bold">
                Password
              </label>
              <input
                onChange={(e) => setPassword(e.target.value)}
                id="password"
                type="password"
                required
                className="p-3 my-2 rounded text-black"
                placeholder="Please enter a strong password"
              />
              <button
                type="submit"
                className="bg-red-700 py-3 my-6 rounded font-bold px-4"
              >
                Submit
              </button>
            </form>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;
