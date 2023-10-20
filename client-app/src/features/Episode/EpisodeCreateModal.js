import { Button, Form, Input,InputNumber , Modal, Radio } from 'antd';
import { useState,useEffect } from 'react';
import TextEditor from '../../components/Common/TextEditor/TextEditor';
import episodeService from '../../services/episodeService'

const EpisodeCreateModal = ({ open, onCancel, movieId, episodeId }) => {

  var constValues = {
    model: {
      name: "Test",
      season:5,
      order: 1,
      duration: 200,
      description: "<b> pls enter your description here </b>",
      id: episodeId,
      seriesId: movieId

    }
  }
  var defaultValues;
  const [form] = Form.useForm();

  useEffect(() => {
    async function fetchMyAPI() {

      defaultValues = constValues;
      if (episodeId != undefined && episodeId != 0) {
        var response = await episodeService.get(episodeId);
        if (response.success) {
          defaultValues.model = response.data;
        }
      }
      debugger;
      form.setFieldsValue(defaultValues);
    }
    fetchMyAPI();
  }, [form, episodeId]);
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
            episodeService.createOrEdit(values.model);
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

        <Form.Item name={["model", "seriesId"]}>
          <Input type="hidden" />
        </Form.Item>
        <Form.Item
         name={["model", "name"]}
          label="Name"
          rules={[
            {
              required: true,
              message: 'Please input the title of collection!',
            },
          ]}
        >
          <Input />

        </Form.Item>

        <Form.Item
          name={["model", "season"]}
          label="Season"
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
          name={["model", "order"]}
          label="Order"
          rules={[
            {
              required: true,
              message: 'Please input the order of episode!',
            },
          ]}
        >
          <InputNumber />
        </Form.Item>
        <Form.Item name={["model", "duration"]} label="Duration">
          <InputNumber addonAfter="min" type="textarea" />
        </Form.Item>

        <Form.Item name={["model", "description"]} label="Description">

<TextEditor />
</Form.Item>
        {/* <Form.Item name="modifier" className="collection-create-form_last-form-item">
          <Radio.Group>
            <Radio value="public">Public</Radio>
            <Radio value="private">Private</Radio>
          </Radio.Group>
        </Form.Item> */}
      </Form>
    </Modal>
  );
};
export default EpisodeCreateModal;