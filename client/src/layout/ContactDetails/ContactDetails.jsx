import {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import axios from "axios";

const baseApiUrl = window.config.apiUrl;
const ContactDetails = (props) => {
    
    
    
    const [contactName, setContactName] = useState('');
    const [contactEmail, setContactEmail] = useState('');
    const [contactPhone, setContactPhone] = useState('');
    
    const {id} = useParams();
    const navigate = useNavigate();
    
    useEffect(() => {
        const url = `${baseApiUrl}/contacts/${id}`;
        axios.get(url).then(
            response => {
                setContactName(response.data.name);
                setContactEmail(response.data.email);
                setContactPhone(response.data.phoneNumber);
            }
        ).catch((error) => {
            navigate("/");
        })
    }, [id, navigate]);
    
    const handleUpdateContact = () => {
        if (window.confirm("Вы уверены, что хотите обновить контакт?"))
        {
            axios.put(`${baseApiUrl}/contacts/${id}`, {name: contactName,  email: contactEmail})
                .then(() => {
                    navigate("/")
                    props.onUpdate();
                })
                .catch((error) => {
                    console.log("Ошибка", error);
                    navigate("/");
                });
        }
    }
    
    return (
        <div className="container mt-5">
            <h2>Детали контакта</h2>
            <div className="mb-3">
                <label className="form-label">Имя: </label>
                <input className="form-control"
                       type="text"
                       value={contactName}
                       onChange={(e) => {
                           setContactName(e.target.value);
                       }}
                />
            </div>
            <div className="mb-3">
                <label className="form-label">Номер телефона: </label>
                <input className="form-control"
                       type="text"
                       value={contactPhone}
                       onChange={(e) => {}}
                />
            </div>
            <div className="mb-3">
                <label className="form-label">Email: </label>
                <input className="form-control"
                       type="text"
                       value={contactEmail}
                       onChange={(e) => {
                           setContactEmail(e.target.value);
                       }}
                />
            </div>

            <button className="btn btn-primary me-2"
                    onClick={(e) => {
                        handleUpdateContact();
                    }}
            >
                Обновить
            </button>
            <button className="btn btn-danger me-2"
                    onClick={(e) => {
                        if (window.confirm("Вы уверены?")) {
                            props.deleteContact(id);
                            navigate("/");
                        }
                    }}
            >
                Удалить
            </button>
            <button className="btn btn-secondary me-2"
                    onClick={(e) => {
                        navigate("/");
                    }}
            >
                Назад
            </button>

        </div>
    )

}

export default ContactDetails;