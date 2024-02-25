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
      .post(`${baseUrl}/user/register`, {
        firstName,
        lastName,
        email,
        password,
      })
      .then((response) => { alert(response.data); window.location.href = "/login"; });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="border flex flex-col h-screen justify-center items-center gap-5">
        <div className="flex gap-3">
          <label htmlFor="firstName">First Name:</label>
          <input id="firstName" type="text" className="border p-1" />
        </div>
        <div className="flex gap-3">
          <label htmlFor="lastName">Last Name:</label>
          <input id="lastName" type="text" className="border p-1" />
        </div>
        <div className="flex gap-3">
          <label htmlFor="email">Email:</label>
          <input id="email" type="email" className="border p-1" />
        </div>
        <div className="flex gap-3">
          <label htmlFor="password">Password:</label>
          <input id="password" type="password" className="border p-1" />
        </div>
        <button type="submit" className="border p-1 bg-black text-white">
          Register
        </button>
      </div>
    </form>
  );
};

export default Register;
