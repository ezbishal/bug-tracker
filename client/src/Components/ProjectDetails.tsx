import axios, { AxiosResponse } from "axios";
import React, { FC, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import ProjectModel from "../Models/ProjectModel";

const ProjectDetails: FC = () => {
    const baseUrl: string = "https://localhost:7109/api"
    const { projectId } = useParams();
    const [project, setProject] = useState<ProjectModel>();

    useEffect(() =>  {
       try{
        axios.get<ProjectModel>(`${baseUrl}/projects/${projectId}`)
            .then((response: AxiosResponse<ProjectModel>) => {
                setProject(response.data);
            });
       } catch(error) {
        alert(`Failed to fetch project: ${error}`);
       }
    }, []);

    return (
        <>
        <div className="flex flex-col gap-5 m-5 w-1/6">

            <input placeholder="projectName" id="name" type="text" 
                className="text-lg"
                value={project?.name} 
                onChange={(e) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, name: e.target.value } : prevState )) } />

            <label htmlFor="description">Description:</label>
            <input id="description" type="text" 
                className="border p-1"
                value={project?.description} 
                onChange={(e) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, description: e.target.value } : prevState )) } />

            <label htmlFor="author">Author:</label>
            <select id="author">
               <option value={project?.authorId}>{project?.authorId}</option>   
            </select>

            <label htmlFor="startDate">Start Date:</label>
            <input id="startDate" type="date" 
                className="border p-1"
                value={project?.startDate.toString()} 
                onChange={(e) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, startDate: new Date(e.target.value)} : prevState))} />

            <label htmlFor="endDate">End Date:</label>
            <input id="endDate" type="date" 
                className="border p-1"
                value={project?.startDate.toString()} 
                onChange={(e) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, endDate: new Date(e.target.value)} : prevState))} />

            <label htmlFor="repository">Repository:</label>
            <input id="repository" type="text" 
                className="border p-1"
                value={project?.name} 
                onChange={(e) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, repositoryLink: e.target.value } : prevState )) } />
        </div>
        </>
    );
}

export default ProjectDetails;