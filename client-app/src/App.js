import React from 'react';
import './App.css';
import {
  BrowserRouter as Router,
  Route,
  Routes
} from "react-router-dom";
import WordLayout from './components/Layout';
import Create from "./components/Create/Create"; 
import List from "./components/List/List";
import WordCardFetch from "./components/WordCard/WordCardFetch" ; 
import GrammerCreate from "./features/Grammer/GrammerCreate";
import GrammerList from "./features/Grammer/GrammerList";
import Home from './components/Home/Home';

function App() {
  return (
    <React.StrictMode>
    <Router>
      <Routes>
        <Route path="/" element={<WordLayout />} />
        <Route  path="/create/:id" element = {<Create />} />
           <Route  path="/list" element = {<List />} />
           <Route  path="/home" element = {<Home />} />
           <Route  path="/test" element = {<WordCardFetch />} />
           <Route  path="/creategrammer/:id" element = {<GrammerCreate />} />
           <Route  path="/listgrammer" element = {<GrammerList />} />
      </Routes>
    </Router>
  </React.StrictMode>

    
  )
}

export default App;