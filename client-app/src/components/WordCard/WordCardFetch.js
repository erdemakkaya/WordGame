import React, { useState, useEffect } from "react";
import WordCard from "./WordCard";
import WordService from '../../services/wordService'

 const WordCardFetch =()=> {
  const [apiData, setAPIData] = useState([]);
  const [loading, setLoading] =useState(false);


  useEffect(() => {
    async function fetchMyAPI() {
        setLoading(true);
      var response=  await WordService.getAllByStatistic();
      console.log(response.data.words);
      setAPIData(response.data.words);
      setLoading(false);
    }
    fetchMyAPI()
  }, [])

  return (
    <div>
      <WordCard loading={loading} setLoading={setLoading}listofapi={apiData} />
    </div>
  );
}

export default WordCardFetch;