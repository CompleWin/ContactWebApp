import {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import axios from "axios";

const baseApiUrl = process.env.REACT_APP_API_URL;
const ContactDetails = (props) => {
    
    const [contact, setContact] = useState({name:"", phoneNumber: "", email: ""});
    const {id} = useParams();
    const navigate = useNavigate();
    
    useEffect(() => {
        const url = `${baseApiUrl}/contacts/${id}`;
        axios.get(url).then(
            response => {
                setContact(response.data)
            }
        ).catch((error) => {
            navigate("/");
        })
    }, [id, navigate]);
    
    return (
        <div className="container mt-5">
            <h2>Детали контакта</h2>
            <div className="mb-3">
                <label className="form-label">Имя: </label>
                <input className="form-control"
                       type="text"
                       value={contact.name}
                       onChange={(e) => {
                       }}
                />
            </div>
            <div className="mb-3">
                <label className="form-label">Номер телефона: </label>
                <input className="form-control"
                       type="text"
                       value={contact.phoneNumber}
                       onChange={(e) => {
                       }}
                />
            </div>
            <div className="mb-3">
                <label className="form-label">Email: </label>
                <input className="form-control"
                       type="text"
                       value={contact.email}
                       onChange={(e) => {
                       }}
                />
            </div>

            <button className="btn btn-primary me-2"
                    onClick={(e) => {
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