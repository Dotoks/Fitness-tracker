function Carousel({ images }) {
  const [currentIndex, setCurrentIndex] = React.useState(0);

  const nextSlide = () => {
    setCurrentIndex((currentIndex + 1) % images.length);
  };

  const prevSlide = () => {
    setCurrentIndex((currentIndex - 1 + images.length) % images.length);
  };

  const transformValue = -currentIndex * 100 + '%';

  return (
    <div className="carousel-container">
      <div className="carousel" style={{ transform: `translateX(${transformValue})` }}>
        {images.map((image, index) => (
          <div className="slide" key={index}>
            <img src={image} alt={`Slide ${index + 1}`} />
          </div>
        ))}
      </div>
      <button className="prev-button" onClick={prevSlide}>❮ Previous</button>
      <button className="next-button" onClick={nextSlide}>Next ❯</button>
    </div>
  );
}
