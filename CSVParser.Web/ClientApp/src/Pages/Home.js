import axios from "axios";
import { useHistory } from "react-router-dom";
import React, {useState, useEffect} from "react";


const Home=()=>{

    const [people, setpeople]=useState([])
   

    useEffect (()=>{
  const getPeople= async()=>{
      const {data}=await axios.get('/api/csv/getpeople')
      setpeople(data)
  }
getPeople();
    }, [])

const onDeleteClick=async()=>{

    await axios.post('/api/csv/deletepeople');
setpeople([])

}
return(
    <div>
    <div className="col-md-12" style={{ marginBottom: 10, marginTop: 30}}>
 
            <button className="btn btn-danger  col-md-12" onClick={onDeleteClick}>Delete All:</button>
     
        </div>
    <br />

    <table className="table table-hover table-striped table-bordered">
        <thead>
            <tr>
            <th>Id:</th>
            <th>First Name:</th>
            <th>Last Name:</th>
            <th>Age:</th>
            <th>Address:</th>
            <th>Email:</th>
         
            </tr>

        </thead>
        <tbody>
      
        {people.map((p, id)=>{
            return <tr key={p.id}>
        <td>{p.id}</td>
        <td>{p.firstName}</td>
        <td>{p.Lastname}</td>
        <td>{p.age}</td>
        <td>{p.address}</td>
        <td>{p.email}</td>
  </tr>
            })}

            </tbody>
</table>
</div>
)

}

export default Home;