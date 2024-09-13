import Requests from "./base/baseservice";

class StatisticService {
    async getAll() {
      let result = await Requests.get('statistic');
      return result;
    }

    
   
  }
  
  export default new StatisticService();