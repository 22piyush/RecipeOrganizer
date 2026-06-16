import { useEffect } from "react";
import { Outlet } from "react-router-dom";

function App() {

  useEffect(()=>{
    console.log("App Mount");
  },[])

  return (
    <>
      <Outlet />
    </>
  );
}

export default App;