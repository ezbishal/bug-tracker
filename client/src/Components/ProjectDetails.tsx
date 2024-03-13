import axios, { AxiosResponse } from "axios";
import React, { FC, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import ProjectModel from "../Models/ProjectModel";
import { ComboBox, DatePicker, Label, TextField } from "@fluentui/react";

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
    }, [projectId]);

    return (
        <>
        <div className="flex flex-col gap-5 m-5 w-1/6">

            <TextField placeholder="projectName" id="name"
                className="text-lg"
                value={project?.name} 
                onChange={(e, v) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, name: v as string} : prevState )) } />

            <Label htmlFor="description">Description:</Label>
            <TextField id="description"
                className="border p-1"
                value={project?.description} 
                onChange={(e, v) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, description: v as string } : prevState )) } />

            <Label htmlFor="author">Author:</Label>
            <ComboBox id="author" options={[]}>
               <option value={project?.authorId}>{project?.authorId}</option>   
            </ComboBox>

            <Label htmlFor="startDate">Start Date:</Label>
            <DatePicker id="startDate" 
                className="border p-1"
                placeholder={project?.startDate.toString()}
                onChange={(d) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, startDate: new Date(d as unknown as Date)} : prevState))} />

            <Label htmlFor="endDate">End Date:</Label>
            <DatePicker id="endDate" 
                className="border p-1"
                placeholder={project?.startDate.toString()} 
                onChange={(d) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, endDate: new Date(d as unknown as Date)} : prevState))} />

            <Label htmlFor="repository">Repository:</Label>
            <TextField id="repository"
                className="border p-1"
                value={project?.name} 
                onChange={(s) => setProject((prevState: ProjectModel | undefined) => (prevState ? { ...prevState, repositoryLink: s as unknown as string } : prevState )) } />
        </div>
        </>
    );
}

export default ProjectDetails;