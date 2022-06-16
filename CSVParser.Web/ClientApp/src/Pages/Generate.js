import React,  { useState } from 'react';



const Generate = () => {

    const [amount, setAmount] = useState(0);

    const onGenerateClick= async ()=>{
    
     window.location.href = `/api/csv/generatecsv/${amount}`;


   }


    return (
        <div className='container mt-8'>
            <div className='row col-md-4 offset-md-6'>
            <input type='text' className='form-control' placeholder='Enter Amount' value={amount} onChange={e=> {setAmount(e.target.value)}} />
            <button onClick={onGenerateClick} className='btn btn-primary' >Generate</button>
            
            </div>
        </div>
    );

}


export default Generate;
