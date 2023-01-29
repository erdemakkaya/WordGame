import Requests from "./base/baseservice";

class GrammerService {
    async getAll() {
      let result = await Requests.get('grammer');
      return result;
    }
    async get(id) {
      let result = await Requests.get(`grammer/${id}`);
      return result;
    }
  
    async getGrammerValues(id) {
      const result = await Requests.get(`grammer/${id}/values`);
      return result;
    }
  
    async createOrEdit(entity) {
      const url = `grammer`;
      let result = await Requests.post(url, entity);
      return result;
    }
  
    async delete(id) {
      let result = await Requests.del(`grammer/${id}`);
      return result;
    }
    async setGrammerValues(id) {
      const result = await Requests.put(`grammer/${id}/values`);
      return result;
    }
  }
  
  export default new GrammerService();