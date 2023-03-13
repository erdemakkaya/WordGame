import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from "react-router-dom";
import { Table, Tag, Typography, Button, Row, Col, Modal } from 'antd';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import WordLayout from '../../components/Layout';
import GrammerService from '../../services/grammerService'





export default function List() {
  const navigate = useNavigate()
  const { Title } = Typography;
  const [APIData, setAPIData] = useState([]);

  async function fetchDeleteGrammer(id) {
    var response = await GrammerService.delete(id);
    if (response.success) {
      setAPIData((pre) => {
        return pre.filter((student) => student.id !== id);
      });
    }
  }

  const onDeleteStudent = (id) => {
    Modal.confirm({
      title: "Are you sure, you want to delete this grammer record?",
      okText: "Yes",
      okType: "danger",
      onOk: () => {
        fetchDeleteGrammer(id);
      },
    });
  };

  const columns = [
    {
      title: 'Name',
      key: 'name',
      dataIndex: 'name',
      render: text => <a>{text}</a>,
    },
    {
      title: 'Category',
      dataIndex: 'category',
    },

    {
      title: 'Tags',
      key: 'tags',
      dataIndex: 'tags',
      render: tags => (
        <>
          {tags.map(tag => {
            let color = tag.length > 5 ? 'geekblue' : 'green';
            if (tag === 'loser') {
              color = 'volcano';
            }
            return (
              <Tag color={color} key={tag}>
                {tag.toUpperCase()}
              </Tag>
            );
          })}
        </>
      ),
    },
    {
      key: "4",
      title: "Actions",
      dataIndex: 'id',
      render: (id) => {
        return (
          <>
            <Link to={`/creategrammer/${id}`}>

              <EditOutlined />
            </Link>

            <DeleteOutlined
              onClick={() => {
                onDeleteStudent(id);
              }}
              style={{ color: "red", marginLeft: 12 }}
            />
          </>
        );
      }
    }
  ];

  useEffect(() => {
    async function fetchMyAPI() {
      var response = await GrammerService.getAll();
      console.log(response.data);
      setAPIData(response.data);
    }
    fetchMyAPI()
  }, [])
  return (
    <WordLayout>
      <div>
        <Row gutter={[40, 0]}>
          <Col span={18}>
            <Title level={2}>
              User List
            </Title>
          </Col>
          <Col span={6}>
            <Button onClick={() => navigate('/creategrammer/0')} block>Add Grammer</Button>
          </Col>
        </Row>
        <Row gutter={[40, 0]}>
          <Col span={24}>
            <Table
              rowKey='id'
              dataSource={APIData}
              columns={columns}
            />
          </Col>
        </Row>
      </div>
    </WordLayout>
  )
}