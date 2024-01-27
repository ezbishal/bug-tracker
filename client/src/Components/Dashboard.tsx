import React, { FC, useState, useEffect } from 'react';
import axios from 'axios';

const Dashboard: FC = () => 
{
    interface User
    {
        id: string;
        firstName: string;
        lastName: string;
    }

    const baseUrl: string = "https://localhost:7109/api";
    const [users, setUsers] = useState<User[]>([]);

    useEffect(() => { 
        try 
        {
             axios.get<User[]>(`${baseUrl}/user`).then((response) => 
             {
                setUsers(response.data);
             });
        } 
        catch (error) 
        {
            console.error("Failed to fetch users:", error); 
        }}, []);
       
    return (
        <div className='App'>
            <h1 className='text-green-900'>Bug Tracker</h1>
            <h2>Users</h2>
            {users.map((user: User) => (
                <div key={user.id}>
                    <h3>{user.firstName}</h3>
                    <h3>{user.lastName}</h3>
                </div>
            ))}
        </div>
    );

}

export default Dashboard;