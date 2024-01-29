import React, { FC, useState, useEffect } from 'react';
import axios, { AxiosResponse } from 'axios';
import ProjectModel from '../Models/ProjectModel';

const Projects: FC = () => 
{
    const baseUrl: string = "https://localhost:7109/api";
    const [projects, setProjects] = useState<ProjectModel[]>([]);

    useEffect(() => {
        try
        {
            axios.get<ProjectModel[]>(`${baseUrl}/projects`).then((response: AxiosResponse<ProjectModel[]>) => {
                setProjects(response.data)
            });
        }
        catch (error)
        {
            console.error("Failed to fetch projects:", error);
        }

    }, []);


    return (
        <div className='App'>
            <h1 className='text-green-900'>Bug Tracker</h1>

            <h2>Projects</h2>
            {projects.map((project: ProjectModel) => (
                <div key={project.id}>
                    <h3>{project.name}</h3>
                </div>
            ))}
        </div>
    );
}

export default Projects;