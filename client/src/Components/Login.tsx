import React, { FC, useState } from "react";
import axios, { AxiosResponse } from "axios";

interface LoginProps {
  setToken: (value: string) => void;
}

const Login: FC<LoginProps> = ({ setToken }) => {
  const [email, setEmail] = useState<string>();
  const [password, setPassword] = useState<string>();

  const baseUrl: string = "https://localhost:7109/api";

  const GetToken = async (
    email: string | undefined,
    password: string | undefined
  ): Promise<string> => {
    try {
      return await axios
        .get(`${baseUrl}/users/token?email=${email}&password=${password}`)
        .then((response: AxiosResponse<string>) => {
          return response.data;
        });
    } catch (error) {
      console.error("Failed to fetch token:", error);
      return "";
    }
  };

  const handleSubmit = async (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
    await GetToken(email, password).then((token: string) => {
      if (token) {
        setToken(token);
        window.location.href = "/";
      } else {
        console.error("Failed to log in");
      }
    });
  };

  function handleSignupClick(event: React.MouseEvent<HTMLDivElement, MouseEvent>): void
  {
    window.location.href = "/register";
  }

  return (
    <div className="bg-gray-400 dark:bg-gray-800 h-screen overflow-hidden flex items-center justify-center">
      <div className="bg-white lg:w-6/12 md:7/12 w-8/12 shadow-3xl rounded-xl">
        <div onClick={handleSignupClick} className="bg-gray-800 shadow shadow-gray-200 absolute left-1/2 transform -translate-x-1/2 -translate-y-1/2 rounded-full p-4 md:p-8">
          <button className="text-white uppercase">Sign up</button>
        </div>
        <form className="p-12 md:p-24">
          <div className="flex items-center text-lg mb-6 md:mb-8">
            <svg className="absolute ml-3" width="24" viewBox="0 0 24 24">
              <path d="M20.822 18.096c-3.439-.794-6.64-1.49-5.09-4.418 4.72-8.912 1.251-13.678-3.732-13.678-5.082 0-8.464 4.949-3.732 13.678 1.597 2.945-1.725 3.641-5.09 4.418-3.073.71-3.188 2.236-3.178 4.904l.004 1h23.99l.004-.969c.012-2.688-.092-4.222-3.176-4.935z" />
            </svg>
            <input
              type="text"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              id="username"
              className="bg-gray-200 rounded pl-12 py-2 md:py-4 focus:outline-none w-full"
              placeholder="Username"
            />
          </div>
          <div className="flex items-center text-lg mb-6 md:mb-8">
            <svg className="absolute ml-3" viewBox="0 0 24 24" width="24">
              <path d="m18.75 9h-.75v-3c0-3.309-2.691-6-6-6s-6 2.691-6 6v3h-.75c-1.24 0-2.25 1.009-2.25 2.25v10.5c0 1.241 1.01 2.25 2.25 2.25h13.5c1.24 0 2.25-1.009 2.25-2.25v-10.5c0-1.241-1.01-2.25-2.25-2.25zm-10.75-3c0-2.206 1.794-4 4-4s4 1.794 4 4v3h-8zm5 10.722v2.278c0 .552-.447 1-1 1s-1-.448-1-1v-2.278c-.595-.347-1-.985-1-1.722 0-1.103.897-2 2-2s2 .897 2 2c0 .737-.405 1.375-1 1.722z" />
            </svg>
            <input
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              type="password"
              id="password"
              className="bg-gray-200 rounded pl-12 py-2 md:py-4 focus:outline-none w-full"
              placeholder="Password"
            />
          </div>
          <button onClick={handleSubmit} className="bg-gradient-to-b from-gray-700 to-gray-900 font-medium p-2 md:p-4 text-white uppercase w-full rounded">
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;
