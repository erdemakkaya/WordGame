import React, { useState } from 'react';
import { Select } from 'antd';

const { Option } = Select;

const TagSelect = ({ options, placeholder, value }) => {
  const [inputValue, setInputValue] = useState('');
  const [selectedTags, setSelectedTags] = useState(value || []);

  const handleInputChange = (value) => {
    setInputValue(value);
  };

  const handleSelectChange = (value) => {
    setSelectedTags(value);
  };

  const handleInputConfirm = () => {
    if (inputValue && !selectedTags.includes(inputValue)) {
      setSelectedTags([...selectedTags, inputValue]);
      setInputValue('');
    }
  };

  return (
    <Select
      mode="tags"
      style={{ width: '100%' }}
      placeholder={placeholder}
      value={selectedTags}
      onChange={handleSelectChange}
      onInputKeyDown={(e) => e.key === 'Enter' && handleInputConfirm()}
    >
      {options.map((option, index) => (
        <Option key={index} value={option}>
          {option}
        </Option>
      ))}
    </Select>
  );
};

export default TagSelect;