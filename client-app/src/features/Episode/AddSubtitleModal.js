import { Button,Upload, Form, Input,InputNumber , Modal, Radio } from 'antd';
import { UploadOutlined} from '@ant-design/icons';

import { useState,useEffect } from 'react';
import TextEditor from '../../components/Common/TextEditor/TextEditor';
import subtitleService from '../../services/subtitleService';

const AddSubtitleModal = ({ open, onCancel, episodeId }) => {

  
  const props = {
    action: 'https://www.mocky.io/v2/5cc8019d300000980a055e76',
    onChange({ file, fileList }) {
      if (file.status !== 'uploading' && file.status !== 'removed') {
        const formData = new FormData();
        formData.append("formFile",file.originFileObj)
        formData.append("fileName",file.name)
        formData.append("episodeId",episodeId)
        subtitleService.createFile(formData);
      }
    },
    defaultFileList: [
      {
        uid: '1',
        name: 'xxx.png',
        status: 'uploading',
        url: 'http://www.baidu.com/xxx.png',
        percent: 33,
      }
      
    ],
  };

 
  return (
    <Modal
    open = {open}
      title="Add Subtitles"
      okText="Done"
      cancelText="Cancel"
      onCancel={onCancel}
      onOk={() => {
        
            // episodeService.createOrEdit(values.model);
      }}
    >
      <Upload {...props}>
    <Button icon={<UploadOutlined />}>Upload</Button>
  </Upload>
    </Modal>
  );
};
export default AddSubtitleModal;