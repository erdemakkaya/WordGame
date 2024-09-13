import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { Row, Col, Typography, Input, Form, Button, Select } from 'antd';
import WordNotification from '../Notification/WordNotification';
import WordService from '../../services/wordService'
import WordLayout from '../Layout';
import TagSelect from '../Common/Select/TagSelect';
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
const { Title } = Typography;

var constValues = {
  model: {
    wordName: "Test",
    turkishTranslator: "Çeviri",
    description: "Description",
    hints: "deneme",
    tags: [
      "Tenses"
    ],
    exampleSentence: "<p>deneme</p>",
    id: 0
  }
}
var defaultValues;

const Create = () => {

  const { id } = useParams();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [imageUrl, setImageUrl] = useState('');

const handleImageUrlChange = (e) => {
  setImageUrl(e.target.value);
};

  const navigate = useNavigate();
  useEffect(() => {
    async function fetchMyAPI() {
      debugger;

      defaultValues = constValues;
      if (id != null && id != 0) {
        var response = await WordService.get(id);
        if (response.success) {
          defaultValues.model = { ...defaultValues.model, ...response.data.word };
        }
      }
      form.setFieldsValue(defaultValues);
       form.setFieldsValue({ 'model.tags': defaultValues.model.tags })
    }
    fetchMyAPI();
  }, [form, id]);




  const onFinish = async (values) => {
    setLoading(true);
    debugger;
    var result = await WordService.createOrEdit(values.model);
    if (result.success) {
      WordNotification.SuccessNotification("İşlem başarılı bir şekilde gerçekleşti.");
    }
    else {
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
            <Title style={{ textAlign: 'center' }} level={2}>
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
                label="Word Name"
                name={['model', 'wordName']}
                rules={[
                  {
                    required: true,
                    message: 'Please input your Word Name!',
                  },
                ]}
              >
                <Input />
              </Form.Item>

              <Form.Item
                label="Translator"
                name={['model', 'turkishTranslator']}
                rules={[
                  {
                    required: true,
                    message: 'Please input your transleted text!',
                  },
                ]}
              >
                <Input />
              </Form.Item>

              <Form.Item
                name={['model', 'type']}
                label="Select"
                hasFeedback
                rules={[
                  {
                    required: true,
                    message: 'Please select a type!',
                  },
                ]}
              >
                <Select placeholder="Please select a type">
                  <Option value="noun">Noun</Option>
                  <Option value="verb">Verb</Option>
                  <Option value="AdVerb">AdVerb</Option>
                  <Option value="Adjective">Adjective</Option>
                </Select>
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
               <TagSelect 
               options={['Important', 'Rare', 'Basic', 'Advanced']}
               placeholder="Please select favourite colors">
                </TagSelect>
              </Form.Item>
              <Form.Item name={['model', 'familiarWords']} label="Familiar Words">
                <Select mode="tags" style={{ width: '100%' }} placeholder="Add words">
                  {/* Add your options here */}
                </Select>
              </Form.Item>

              <Form.Item name={['model', 'imageUrl']} label="Image URL">
                <Input placeholder="Enter image URL" onChange={handleImageUrlChange} />
              </Form.Item>

              <Form.Item label="Image Preview">
                <img src={form.getFieldValue(['model', 'imageUrl'])} alt="" style={{ maxWidth: '100%' }} />
              </Form.Item>

              <Form.Item name={['model', 'description']} label="Introduction">
                <Input.TextArea />
              </Form.Item>

              <Form.Item name={['model', 'exampleSentence']} label="Example Sentence">
                <Input.TextArea />
              </Form.Item>

              <Form.Item
                wrapperCol={{
                  offset: 8,
                  span: 16,
                }}
              >
                <div style={{ textAlign: "right" }}>
                  <Button loading={loading} type="primary" htmlType="submit">
                    Submit
                  </Button> {' '}
                  <Button type="danger" htmlType="button" onClick={() => navigate('/list')}>
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

export default Create;
