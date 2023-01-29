import React, { useState,useEffect } from 'react';
import { useNavigate,useParams } from "react-router-dom";
import {Row, Col, Typography, Input, Form, Button,Select} from 'antd';
  import WordNotification from '../../components/Notification/WordNotification';
  import GrammerService from '../../services/grammerService'
  import TextEditor from '../../components/Common/TextEditor/TextEditor'
import WordLayout from '../../components/Layout';
const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 8 },
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 16 },
  },
};

const { Option } = Select;
const {Title} = Typography;
var constValues ={
  model:{ name: "Test",
  category: "Test2",
  basicRules: "<b>deneme</b>",
  hints: "<b>deneme</b>",
  tags: [
    "Tenses"
],
  exampleSentence: "<p>deneme</p>",
  id: 0
}
}
var defaultValues;

const GrammerCreate = ()  => {
const { id } = useParams();
const [form] = Form.useForm();

  useEffect(() => {
      async function fetchMyAPI() {
        
        defaultValues=constValues;
        if(id!=null && id!=0 )
        {
        var response =  await  GrammerService.get(id);
        if(response.success){
        defaultValues.model=response.data;
        }
      }
      form.setFieldsValue(defaultValues);
    }
      fetchMyAPI();
   }, [form,id]);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate()
    
  const onFinish = async (values) => {
    setLoading(true);
    console.log(values);
   var result=await GrammerService.createOrEdit(values.model);
   if(result.success) {
     WordNotification.SuccessNotification("İşlem başarılı bir şekilde gerçekleşti.");
   }
   else{
    WordNotification.ErrorNotification("Bir Hata ile karşılaşıldı.");

   }
  setLoading(false);
  };

  const onFinishFailed = (errorInfo) => {
    WordNotification.ErrorNotification('you encountered an error');
  };

  return (
    <WordLayout>
    <div>
    <Row gutter={[40, 0]}>
      <Col span={23}>
        <Title style={{textAlign: 'center'}} level={2}>
        Please Fill the User Form
        </Title>
        </Col>
    </Row>
    <Row gutter={[40, 0]}>
    <Col span={18}>
    <Form
    {...formItemLayout}
      name="basic"
      form={form}
      onFinish={onFinish}
      onFinishFailed={onFinishFailed}
      autoComplete="off"
      initialValues={defaultValues}
    >
       <Form.Item name={["model","id"]}>
        <Input type="hidden"  />
    </Form.Item>
      <Form.Item
        label="Grammer Name"
        name={['model', 'name']}
        rules={[
          {
            required: true,
            message: 'Please input your Word Name!',
          },
        ]}
      >
        <Input  />
      </Form.Item>

      <Form.Item
        label="Category"
        name={['model', 'category']}
        rules={[
          {
            required: true,
            message: 'Please input your transleted text!',
          },
        ]}
      >
        <Input  />
      </Form.Item>

 

      <Form.Item
        name={['model', 'tags']}
        label="Tags[multiple]"
        rules={[
          {
            required: true,
            message: 'Please select your Tags!',
            type: 'array',
          },
        ]}
      >
        <Select mode="multiple" placeholder="Please select favourite colors">
          <Option value="Tenses">Tenses</Option>
          <Option value="Adjectives">Adjectives</Option>
          <Option value="Grammar">Grammar</Option>
          <Option value="VOCABULARY">VOCABULARY</Option>
          <Option value="Nouns">Nouns</Option>
        </Select>
      </Form.Item>

      <Form.Item name={['model', 'basicRules']} label="Basic Rules">
     
          <TextEditor />
      </Form.Item>

      <Form.Item name={['model', 'hints']} label="Hints"
       rules={[
        {
          required: true,
          message: 'Please input your Hints!',
        },
      ]}
      >
      <TextEditor />
      </Form.Item>


      <Form.Item name={['model', 'exampleSentence']} label="Example Sentence">
      <TextEditor />
      </Form.Item>

      <Form.Item
        wrapperCol={{
          offset: 8,
          span: 16,
        }}
      >
         <div style={{textAlign: "right"}}>
        <Button loading={loading}   type="primary" htmlType="submit">
          Submit
        </Button> {' '}
        <Button type="danger" htmlType="button" onClick={() => navigate('/listgrammer')}>
              Back
            </Button>
            </div>
      </Form.Item>
    </Form>
    </Col>
        </Row>
    </div>
    </WordLayout>
  );
};

export default GrammerCreate;
