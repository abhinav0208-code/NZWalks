import "./App.css";
import { LoginContainer } from "./Containers/LoginContainer";
import { Route, Routes } from "react-router-dom";
import { RegisterUserContainer } from "./Containers/RegisterUserContainer";
import { RegionContainer } from "./Containers/RegionsContainer";

function App() {
  return (
    <Routes>
      <Route path="/Region" element={<RegionContainer />} />
      <Route path="/Register" element={<RegisterUserContainer />} />
      <Route path="/" element={<LoginContainer />} />
    </Routes>
  );
}

export default App;
