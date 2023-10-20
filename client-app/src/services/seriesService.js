import Requests from "./base/baseservice";

class seriesService {
    async getAll() {
      let result = await Requests.get('series');
      return result;
    }
    async get(id) {
      let result = await Requests.get(`series/${id}`);
      return result;
    }
  
    async getGrammerValues(id) {
      const result = await Requests.get(`series/${id}/values`);
      return result;
    }
  
    async createOrEdit(entity) {
      const url = `series`;
      let result = await Requests.post(url, entity);
      return result;
    }
  
    async delete(id) {
      let result = await Requests.del(`series/${id}`);
      return result;
    }
    async setGrammerValues(id) {
      const result = await Requests.put(`series/${id}/values`);
      return result;
    }
  }
  
  export default new seriesService();