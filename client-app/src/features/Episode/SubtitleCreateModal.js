import { Button, Form, Input,InputNumber , Modal,Switch } from 'antd';
import { useState,useEffect } from 'react';
import subtitleService from '../../services/subtitleService'

const EpisodeCreateModal = ({ open, onCancel,id }) => {

  var constValues = {
    model: {
      text: "Test",
      turkishText:"Turkish Translation of text",
      section: 0,
      startTime: "00:00:35.3020000",
      endTime: "00:00:35.3020000",
      isFavourite: false,
      episodeId: 0,
      id: id

    }
  }
  var defaultValues;
  const [form] = Form.useForm();

  useEffect(() => {
    async function fetchMyAPI() {
debugger;
      defaultValues = constValues;
      if (id != undefined && id != 0) {
        var response = await subtitleService.getBySubId(id);
        if (response.success) {
          defaultValues.model = response.data;
        }
      }
      debugger;
      form.setFieldsValue(defaultValues);
    }
    fetchMyAPI();
  }, [form, id]);
  return (
    <Modal
      open={open}
      title="Create a new collection"
      okText="Create"
      cancelText="Cancel"
      onCancel={onCancel}
      onOk={() => {
        form
          .validateFields()
          .then((values) => {
            form.resetFields();
            subtitleService.createOrEdit(values.model);
          })
          .catch((info) => {
            console.log('Validate Failed:', info);
          });
      }}
    >
      <Form
        form={form}
        layout="vertical"
        name="form_in_modal"
        initialValues={{
          modifier: 'public',
        }}
      >

<Form.Item name={["model", "id"]}>
          <Input type="hidden" />
        </Form.Item>

       
        <Form.Item
         name={["model", "text"]}
          label="Text"
          rules={[
            {
              required: true,
              message: 'Please input the text of collection!',
            },
          ]}
        >
          <Input />

        </Form.Item>

        <Form.Item
         name={["model", "turkishText"]}
          label="Turkish Tranlsation"
          rules={[
            {
              required: true,
              message: 'Please input the translation of text!',
            },
          ]}
        >
          <Input />

        </Form.Item>

        <Form.Item
          name={["model", "section"]}
          label="Section"
          rules={[
            {
              required: true,
              message: 'Please input the season of episode!',
            },
          ]}
        >
          <InputNumber />
        </Form.Item>

        <Form.Item
         name={["model", "startTime"]}
          label="Start Time"
          rules={[
            {
              required: true,
              message: 'Please input the translation of text!',
            },
          ]}
        >
          <Input />

        </Form.Item>

        <Form.Item
         name={["model", "endTime"]}
          label="End Time"
          rules={[
            {
              required: true,
              message: 'Please input the translation of text!',
            },
          ]}
        >
          <Input />

        </Form.Item>

        <Form.Item
          name={["model", "isFavourite"]}
          valuePropName="checked"
          label="Is Favourite"
          rules={[
            {
              required: true,
              message: 'Please input the order of episode!',
            },
          ]}
        >
          <Switch  />
        </Form.Item>
        <Form.Item
          name={["model", "episodeId"]}
          label="EpisodeId"
          rules={[
            {
              required: true,
              message: 'Please input the season of episode!',
            },
          ]}
        >
          <InputNumber />
        </Form.Item>
       
      </Form>
    </Modal>
  );
};
export default EpisodeCreateModal;