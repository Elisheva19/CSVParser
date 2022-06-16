import React , {useState, useRef} from "react";
import { useHistory } from "react-router-dom";
import axios from 'axios';


const Upload=()=>{

    const fileInputRef=useRef(null)
    const history=useHistory();

const toBase64= file=> new Promise((resolve, reject)=>{
    const reader= new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});
    


const onUploadClick = async ()=>{
 const file= fileInputRef.current.files[0];
 const base64 = await toBase64(file);
 const name = file.name;
 await axios.post('/api/csv/upload', {Base64File: base64 ,name})
 history.push('/')



}


    return(
<div className="container mt-5">
    <div className="row">
        <div className="'col-md-12">
            <input ref={fileInputRef} type='file' className="form-control" placeholder="Chose File!!!"/>
            </div>
            <div className="col-md-12">
                <button className="btn btn-outline-danger" onClick={onUploadClick}>Upload</button>
        </div>
    </div>
</div>

    )
}


export default Upload;