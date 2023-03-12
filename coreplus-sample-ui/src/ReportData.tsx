
const ReportData=({reportData})=>{
    return (
        <>
       {reportData.map(x => (
           
           <tr key={x.id}>
           <td>{x.pracName}</td>
           <td>{x.revenue}</td>
           <td>{x.cost}</td>
           <td>{x.month}/{x.year}</td> 
       </tr>
       ))}
        

        
        </>
    )
}
export default ReportData;

