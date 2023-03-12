import "./app.css";
import { useEffect, useState } from "react";  
import ReportData from "./ReportData";
const supervisorsAPI="https://localhost:44348/practitioners/supervisors";
const practitionersAPI="https://localhost:44348/practitioners";
const ReportAPI="https://localhost:44348/practitioners/GetAppointmentList?startDate=1/1/2010&endDate=12/12/2023";
function App() {

  const [practitioners,setPractitioners]=useState([]);
  const [supervisors,setsupervisors]=useState([]);
  const [reportData,setReportData]=useState([]);
  const fetchPractitioners= async(urlVal: RequestInfo | URL)=>{
    try{
      const pracResponse=await fetch(urlVal);
      const data=await pracResponse.json();
      if(data.length>0){
        setPractitioners(data);
      }
      console.log(data);
    }catch(e){
      console.error(e);
    }
  }
  const fetchsupervisors= async(urlVal: RequestInfo | URL)=>{
    try{
      const pracResponse=await fetch(urlVal);
      const data=await pracResponse.json();
      if(data.length>0){
        setsupervisors(data);
      }
      console.log(data);
    }catch(e){
      console.error(e);
    }
  }
  useEffect(()=>{
    fetchPractitioners(practitionersAPI);
    fetchsupervisors(supervisorsAPI);
    fetchReportDate(ReportAPI)
  },[])
  const fetchReportDate= async(urlVal: RequestInfo | URL)=>{
    try{
      const pracResponse=await fetch(urlVal);
      const data=await pracResponse.json();
      if(data.length>0){
        setReportData(data);
      }
      console.log(data);
    }catch(e){
      console.error(e);
    }
  } 
  return (
    <div className="h-screen w-full appshell">
      <div className="header flex flex-row items-center p-2 bg-primary shadow-sm">
        <p className="font-bold text-lg">coreplus</p>
      </div>
      <div className="supervisors">
        <h4><b>Supervisor practitioners</b></h4>
        <ul>
        {supervisors.map(x => (
           
                    <li key={x.id}>
                        {x.name}
                    </li>
                ))}
        
        </ul>
        </div>
      <div className="praclist"> <h4><b>Remaining Practitioners</b></h4>
      {practitioners.map(x => (
           
           <li key={x.id}><a>
               {x.name}
           </a>
           </li>
       ))}
      </div>
      <div className="pracinfo">Practitioner Report UI
      <table>
        <thead>
        <tr>
                    <th>PracName</th>
                    <th>Revenue</th>
                    <th>Cost</th>
                    <th>Date</th>
                </tr>
        </thead>
        <tbody>
          
          <ReportData reportData={reportData} />

        </tbody>
      </table>
      </div>
      
    </div>
  );
}

export default App;
