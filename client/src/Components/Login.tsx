import React, { FC, FormEvent, useState } from "react";
import axios, { AxiosResponse } from "axios";

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

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    await GetToken({ username, password }).then((token: string) => {
      if (token) {
        setToken(token);
        window.location.href = "/";
      } else {
        console.error("Failed to log in");
      }
    });
  };

  return (
    <>
      <div className="relative mx-auto w-full max-w-md bg-white px-6 pt-10 pb-8 shadow-xl ring-1 ring-gray-900/5 sm:rounded-xl sm:px-10">
        <div className="w-full">
          <div className="text-center">
            <h1 className="text-3xl font-semibold text-gray-900">Sign in</h1>
            <p className="mt-2 text-gray-500">
              Sign in to access your account
            </p>
          </div>
          <div className="mt-5">
            <form action="">
              <div className="relative mt-6">
                <input
                  type="text"
                  id="username"
                  placeholder="Username"
                  className="peer mt-1 w-full border-b-2 border-gray-300 px-0 py-1 placeholder:text-transparent focus:border-gray-500 focus:outline-none"
                />
                <label
                  htmlFor="username"
                  className="pointer-events-none absolute top-0 left-0 origin-left -translate-y-1/2 transform text-sm text-gray-800 opacity-75 transition-all duration-100 ease-in-out peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-500 peer-focus:top-0 peer-focus:pl-0 peer-focus:text-sm peer-focus:text-gray-800"
                >
                  Username
                </label>
              </div>
              <div className="relative mt-6">
                <input
                  type="password"
                  id="password"
                  placeholder="Password"
                  className="peer peer mt-1 w-full border-b-2 border-gray-300 px-0 py-1 placeholder:text-transparent focus:border-gray-500 focus:outline-none"
                />
                <label
                  htmlFor="password"
                  className="pointer-events-none absolute top-0 left-0 origin-left -translate-y-1/2 transform text-sm text-gray-800 opacity-75 transition-all duration-100 ease-in-out peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-500 peer-focus:top-0 peer-focus:pl-0 peer-focus:text-sm peer-focus:text-gray-800"
                >
                  Password
                </label>
              </div>
              <div className="my-6">
                <button type="submit" className="w-full rounded-md bg-black px-3 py-4 text-white focus:bg-gray-600 focus:outline-none" >
                  Sign in
                </button>
              </div>
              <p className="text-center text-sm text-gray-500">
                Don't have an account yet?
                <a href="#!" className="font-semibold text-gray-600 hover:underline focus:text-gray-800 focus:outline-none" >
                  Sign up
                </a>
              </p>
            </form>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;
