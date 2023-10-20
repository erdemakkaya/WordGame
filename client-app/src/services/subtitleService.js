import Requests from "./base/baseservice";

class SubtitleService {

  async get (){
    return await Requests.get(`subtitle`);
  }
  async getByEpisodeId(id)
  {
    let result = await Requests.get(`subtitle/${id}`);
    return result;
  }
  
    async createFile(file) {
      const url = `subtitle/`;
      let result = await Requests.postFile(url, file);
      return result;
    }
  
    async delete(id) {
      let result = await Requests.del(`subtitle/${id}`);
      return result;
    }
    async setGrammerValues(id) {
      const result = await Requests.put(`subtitle/${id}/values`);
      return result;
    }

    async getBySubId(id) {
      let result = await Requests.get(`subtitle/sub/${id}`);
      return result;
    }

      async createOrEdit(entity) {
        const url = entity.id ? `subtitle/${entity.id}` : `subtitle`;
        let result = entity.id ? await Requests.put(url, entity) : 
                          await Requests.post(url, entity);
        return result;
      }
  }
  
  export default new SubtitleService();