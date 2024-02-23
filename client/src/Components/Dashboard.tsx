import React, { FC } from 'react';
import Projects from './Projects'

const Dashboard: FC = () => 
{
    return (
        <div>
            <h1 className='text-white font-bold text-2xl'>Dashboard</h1>
        <Projects />
        </div>
    );        
}

export default Dashboard;
