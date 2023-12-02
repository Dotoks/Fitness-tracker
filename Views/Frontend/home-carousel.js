function Carousel({ images }) {
    const [currentIndex, setCurrentIndex] = React.useState(0);
    const [startX, setStartX] = React.useState(0);
    const [isDragging, setIsDragging] = React.useState(false);
  
    const updateCarousel = () => {
      const newTransformValue = -currentIndex * 100 + '%';
      return { transform: `translateX(${newTransformValue})` };
    };
  
    const nextSlide = () => {
      setCurrentIndex((currentIndex + 1) % images.length);
    };
  
    const prevSlide = () => {
      setCurrentIndex((currentIndex - 1 + images.length) % images.length);
    };
  
    const handleMouseDown = (e) => {
      e.stopPropagation();
      setIsDragging(true);
      setStartX(e.pageX - document.querySelector('.carousel').offsetLeft);
    };
  
    const handleMouseMove = (e) => {
      if (!isDragging) return;
      const x = e.pageX - document.querySelector('.carousel').offsetLeft;
      const walk = (x - startX) * 2; 
      document.querySelector('.carousel').scrollLeft = document.querySelector('.carousel').scrollLeft - walk;
    };
  
    const handleMouseUp = () => {
      setIsDragging(false);
    };
  
    return (
      <div className="carousel-container">
        <div
          className="carousel"
          style={updateCarousel()}
          onMouseDown={handleMouseDown}
          onMouseMove={handleMouseMove}
          onMouseUp={handleMouseUp}
        >
          {images.map((image, index) => (
            <div className="slide" key={index}>
              <img src={image} alt={`Slide ${index + 1}`} />
            </div>
          ))}
        </div>
        <button className="prev-button" onClick={prevSlide}>
          Previous
        </button>
        <button className="next-button" onClick={nextSlide}>
          Next
        </button>
      </div>
    );
  }
  
  window.Carousel = Carousel;
  