import axios from "axios";
import React from "react";

const Register: React.FC = () => {
  const baseUrl: string = "https://localhost:7109/api";

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const firstName = (document.querySelector("#firstName") as HTMLInputElement)
      .value;
    const lastName = (document.querySelector("#lastName") as HTMLInputElement)
      .value;
    const email = (document.querySelector("#email") as HTMLInputElement).value;
    const password = (document.querySelector("#password") as HTMLInputElement)
      .value;

    axios
      .post(`${baseUrl}/users/register`, {
        firstName,
        lastName,
        email,
        password,
      })
      .then((response) => {
        window.location.href = "/login";
      });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="flex items-center justify-center h-screen">
        <div className="border-2 rounded-md p-3 flex flex-col w-fit gap-5 m-auto">
          <div>
            <label htmlFor="firstName" className="mr-5">First Name:</label>
            <input id="firstName" type="text" className="border mr-5" />
          </div>
          <div>
            <label htmlFor="lastName" className="mr-5">Last Name:</label>
            <input id="lastName" type="text" className="border p-1" />
          </div>
          <div>
            <label htmlFor="email" className="mr-5">Email:</label>
            <input id="email" type="email" className="border p-1" />
          </div>
          <div>
            <label htmlFor="password" className="mr-5">Password:</label>
            <input id="password" type="password" className="border p-1" />
          </div>
          <button type="submit" className="border p-1 bg-black text-white">
            Register
          </button>
        </div>
        </div>
    </form>
  );
};

export default Register;
