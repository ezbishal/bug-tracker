import { memo, useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import React from "react";
import UserSession from "../Models/UserSession";

const Users = () => {
  const baseUrl: string = "https://localhost:7109/api";
  const [users, setUsers] = useState<UserSession[]>([]);
  const [selectedUser, setSelectedUser] = useState<UserSession | null>(null);
  const [firstName, setFirstName] = useState<string>('');
  const [lastName, setLastName] = useState<string>('');

  useEffect(() => {
    axios
      .get(`${baseUrl}/users`)
      .then((response: AxiosResponse<UserSession[]>) => {
        console.log(response.data);
        setUsers(response.data);
        setSelectedUser(response.data[0]);
      });
  }, []);

  const handleSelectionChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    const user = users.find(
      (user: UserSession) => user.email === event.target.value
    );
    if (user) {
      setSelectedUser(user);
      setFirstName(user.firstName);
      setLastName(user.lastName);
    }
  };

  function handleDelete(
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ): void {
    if (!selectedUser) return;
    const email = selectedUser.email;
    axios
      .delete(`${baseUrl}/users/${email}`)
      .then((response: AxiosResponse<string>) => {
        alert(response.data);
        window.location.href = "/";
      });
  }

  function handleSave(event: React.MouseEvent<HTMLButtonElement, MouseEvent>): void
  {
    if (!selectedUser) return;
    const email = selectedUser.email;
    const user = {firstName, lastName};
    axios
      .put(`${baseUrl}/users/${email}`, user)
      .then((response: AxiosResponse<string>) => {
        alert(response.data);
        window.location.href = "/";
      });
  }

  return (
    <div className="flex flex-col gap-5 m-5">
      <h3 className="text-[20px]">Users</h3>
        <label htmlFor="users">Select User:</label>
        <select id="users" className="border" onChange={handleSelectionChange}>
          {users.map((user: UserSession) => (
            <option value={user.email}>
              {user.firstName} {user.lastName}
            </option>
          ))}
        </select>

        <label htmlFor="firstName">First Name:</label>
        <input id="firstName" className="border" type="text" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
        <label htmlFor="lastName">Last Name:</label>
        <input id="lastName" className="border" type="text" value={lastName} onChange={(e) => setLastName(e.target.value)} />
        <button className="border" onClick={handleSave}>
          Save
        </button>

        <button className="border" onClick={handleDelete}>
          Delete
        </button>
    </div>
  );
};

export default memo(Users);