import React, { useState, useEffect } from 'react';
import { useNavigate, useParams,Link } from "react-router-dom";
import { Row, Col, Typography, Input, Form, Button, Select, Tabs, Avatar, List,Divider, Space,InputNumber } from 'antd';
import { UserOutlined,DatabaseTwoTone,EditOutlined } from '@ant-design/icons';
import WordNotification from '../../components/Notification/WordNotification';
import SeriesService from '../../services/seriesService'
import TextEditor from '../../components/Common/TextEditor/TextEditor'
import WordLayout from '../../components/Layout';
import EpisodeCreateModal from '../Episode/EpisodeCreateModal';
import AddSubtitleModal from '../Episode/AddSubtitleModal';
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
var constValuesOfMovie = {
    model: {
        name: "Test",
        duration: 200,
        episodes : {},
        description: "<b> pls enter your description here </b>",
        totalSeason: 10,
        hints: "<b>deneme</b>",
        tags: [
            "Action"
        ],
        id: 0
    }
}
var defaultValuesOfMovie;

var mappedEpisodes;
const MovieCreate = () => {
    const { id } = useParams();
    const [form] = Form.useForm();
    const [season, setSeason] = useState('0');
    const [selectedEpisodeId, setSelectedEpisodeId] = useState('0');

    useEffect(() => {
        async function fetchMyAPI() {

            defaultValuesOfMovie = constValuesOfMovie;
            if (id != null && id != 0) {
                var response = await SeriesService.get(id);
                if (response.success) {
                    console.log(response.data);
                    defaultValuesOfMovie.model = response.data;
                    debugger;
                    mappedEpisodes = Object.values(defaultValuesOfMovie.model.episodes)
                    setAPIData(mappedEpisodes);
                    setIsSubmitted(true);
                }
            }
            form.setFieldsValue(defaultValuesOfMovie);

        }
        fetchMyAPI();
    }, [form, id]);

    function openSubsModal(epId){
        debugger;
        setSelectedEpisodeId(epId);
        setOpenSubtitle(true);
    }
    useEffect(() => {
        async function filterEpisodes() {

            debugger;
            if (mappedEpisodes!= null) {
                let filteredEpisodes = mappedEpisodes;
                    if(season!= 0){
                        filteredEpisodes = filteredEpisodes.filter(x=>  x.season==season);
                    }
                    setAPIData(filteredEpisodes);
                }
            }
           
        filterEpisodes();
    }, [season]);
    const [loading, setLoading] = useState(false);
    const [open, setOpen] = useState(false);
    const [openSubtitle, setOpenSubtitle] = useState(false);
    const [isSubmitted, setIsSubmitted] = useState(false);
    const [APIData, setAPIData] = useState([]);
    const [mode, setMode] = useState('top');
    const navigate = useNavigate()
    const onFinish = async (values) => {
        setLoading(true);
        var result = await SeriesService.createOrEdit(values.model);
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
                            initialValues={defaultValuesOfMovie}
                        >
                            <Form.Item name={["model", "id"]}>
                                <Input type="hidden" />
                            </Form.Item>
                            <Form.Item
                                label="Movie Name"
                                name={['model', 'name']}
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
                                label="Duration"
                                name={['model', 'duration']}
                                rules={[
                                    {
                                        required: true,
                                        message: 'Please input duration of movie/series!',
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>

                            <Form.Item
                                label="Total Seasons"
                                name={['model', 'totalSeason']}
                                rules={[
                                    {
                                        required: true,
                                        message: 'Please input duration of movie/series!',
                                    },
                                ]}
                            >
                                <Input />
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
                                    <Option value="Action">Action</Option>
                                    <Option value="Adventure">Adventure</Option>
                                    <Option value="Animated">Animated</Option>
                                    <Option value="Comedy">Comedy</Option>
                                    <Option value="Drama">Drama</Option>
                                    <Option value="Fantasy">Fantasy</Option>
                                    <Option value="Historical">Historical</Option>
                                    <Option value="Horror">Horror</Option>
                                    <Option value="Musical">Musical</Option>
                                    <Option value="Science Fiction">Science Fiction</Option>
                                    <Option value="Thriller Film">Thriller Film</Option>
                                    <Option value="Western">Western</Option>
                                </Select>
                            </Form.Item>

                            <Form.Item name={['model', 'description']} label="Description">

                                <TextEditor />
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
                                    </Button>
                                    <Button loading={loading} disabled={id == 0} onClick={() => setOpen(true)} active={id != 0} type="success">
                                        Add Episode
                                    </Button>
                                    {' '}
                                    <Button type="danger" htmlType="button" onClick={() => navigate('/listmovie')}>
                                        Back
                                    </Button>
                                </div>
                            </Form.Item>


                        </Form>
                    </Col>
                </Row>
            </div>
            <Divider>Episodes</Divider>
            <Space>
      <InputNumber prefix="Season" addonBefore={<DatabaseTwoTone twoToneColor="#52c41a" />} min={0} max={10} value={season} onChange={setSeason} />
      <Button
        type="primary"
        onClick={() => {
          setSeason(0);
        }}
      >
        Reset
      </Button>
      
    </Space>
    <Divider/>
            {isSubmitted && <List
      itemLayout="horizontal"
      dataSource={APIData}
      renderItem={(item, index) => (
       
        <List.Item   actions={[<Button key="list-loadmore-edit" onClick ={ () =>openSubsModal(item.id)}>Add Subs </Button>,  <Link  to={`/listsubtitle/${item.id}`}>
            Subtitles
        <EditOutlined />
      </Link>]}>
          <List.Item.Meta
            avatar={<Avatar src={`https://xsgames.co/randomusers/avatar.php?g=pixel&key=${index}`} />}
            title={<a href="https://ant.design">{item.name}</a>}
            description={item.description}
          />
        </List.Item>
      )}
    />
            }
            <EpisodeCreateModal
                open={open}
                onCancel={() => {
                    setOpen(false);
                }}
                movieId={id}
                episodeId={0}
            />
            {
                selectedEpisodeId!=0 &&
<AddSubtitleModal
                open={openSubtitle}
                onCancel={() => {
                    setOpenSubtitle(false);
                }}
                episodeId={selectedEpisodeId}
            />
}

        </WordLayout>
    );
};

export default MovieCreate;
