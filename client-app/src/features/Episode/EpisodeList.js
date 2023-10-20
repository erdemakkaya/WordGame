import { Avatar, List } from 'antd';

const EpisodeList = (data) => (
    <List
      itemLayout="horizontal"
      dataSource={data}
      renderItem={(item, index) => (
        <List.Item>
          <List.Item.Meta
            avatar={<Avatar src={`https://xsgames.co/randomusers/avatar.php?g=pixel&key=${index}`} />}
            title={<a href="https://ant.design">{item.name}</a>}
            description={item.description}
          />
        </List.Item>
      )}
    />
  );
  export default EpisodeList;